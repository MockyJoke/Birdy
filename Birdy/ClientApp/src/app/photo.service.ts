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
    return this.http.get<any>(`${this.endpointUrl}api/photos/${albumSetId}`).pipe(map(albumSetResult => {
      var albumSet = new AlbumSet(albumSetResult.id, albumSetResult.name);
      albumSet.setAlbums(albumSetResult['albums'].map(album => new Album(albumSetResult.id, albumSetResult.name, album.id, album.name)));
      return albumSet;
    }));
  }

  getAlbum(albumSetId: string, albumId: string): Observable<Album> {
    return this.http.get<any>(`${this.endpointUrl}api/photos/${albumSetId}/${albumId}`).pipe(map(albumResult => {
      var album = new Album(albumResult.albumCollectionId, albumResult.albumCollectionName, albumResult.id, albumResult.name);
      album.setPhotos(albumResult['photos'].map(photo => new Photo(
        albumResult.albumCollectionId,
        albumResult.albumCollectionName,
        albumResult.id,
        albumResult.name,
        photo.id,
        photo.name
      )));
      return album;
    }));
  }

  getPhoto(albumSetId: string, albumId: string, photoId: string): Observable<Photo> {
    return this.http.get<any>(`${this.endpointUrl}api/photos/${albumSetId}/${albumId}/${photoId}`).pipe(map(photoResult => {
      var photo = new Photo(photoResult.albumCollectionId, photoResult.albumCollectionName, photoResult.albumId, photoResult.albumName, photoResult.id, photoResult.name);
      return photo;
    }));
  }

  generateThumbnailPhotoUrl(albumSetId: string, albumId: string, photoId: string): string {
    return `${this.endpointUrl}api/photos/${albumSetId}/${albumId}/${photoId}/mini`;
  }
  generateHdPhotoUrl(albumSetId: string, albumId: string, photoId: string): string {
    return `${this.endpointUrl}api/photos/${albumSetId}/${albumId}/${photoId}/hd`;
  }
}
