import { Injectable } from '@angular/core';
import { AlbumSet } from './models/album-set';
import { Observable, of, from } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Album } from './models/album';
import { Photo } from './models/photo';
import { Root } from './models/root';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  private endpointUrl: string;
  constructor(private http: HttpClient) {
    this.endpointUrl = "";
  }


  getRoot(): Observable<Root> {
    return this.http.get<any[]>(this.endpointUrl + "/api/photos").pipe(map(res => {
      var albumSets = res.map(albumSet => new AlbumSet(albumSet.id, albumSet.name))
      var root = new Root(albumSets);
      return root;
    }));
  }

  getAlbumSet(albumSetId: string): Observable<AlbumSet> {
    return this.http.get<any>(`${this.endpointUrl}api/photos/${albumSetId}`).pipe(map(res => {
      var albumSet = new AlbumSet(res.id, res.name);
      albumSet.setAlbums(res['albums'].map(album => new Album(res['id'], album.id, album.name)));
      return albumSet;
    }));
  }

  getAlbum(albumSetId: string, albumId: string): Observable<Album> {
    return this.http.get<any>(`${this.endpointUrl}api/photos/${albumSetId}/${albumId}`).pipe(map(res => {
      var album = new Album(res.albumCollectionId, res.id, res.name);
      album.setPhotos(res['photos'].map(photo => new Photo(
        res['albumCollectionId'],
        res['id'],
        photo.id,
        photo.name
      )));
      return album;
    }));
  }

  generateThumbnailPhotoUrl(albumSetId: string, albumId: string, photoId: string): string {
    return `${this.endpointUrl}api/photos/${albumSetId}/${albumId}/${photoId}/mini`;
  }
}
