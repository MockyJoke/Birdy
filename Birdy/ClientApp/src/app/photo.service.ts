import { Injectable } from '@angular/core';
import { AlbumSet } from './models/album-set';
import { Observable, of, from } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Album } from './models/album';
import { Photo } from './models/photo';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  private endpointUrl: string;
  constructor(private http: HttpClient) {
    this.endpointUrl = "";
  }

  getAlbumSets(): Observable<AlbumSet[]> {
    return this.http.get<any[]>(this.endpointUrl + "/api/photos").pipe(map(res => {
      return res.map(albumSet => new AlbumSet(albumSet.id, albumSet.name))
    }));
  }

  getAlbums(albumSetId: string): Observable<Album[]> {
    return this.http.get(`${this.endpointUrl}api/photos/${albumSetId}`).pipe(map(res => {
      return res['albums'].map(album => new Album(res['albumCollectionId'], album.id, album.name));
    }));
  }

  getPhotos(albumSetId: string, albumId: string): Observable<Photo[]> {
    return this.http.get(`${this.endpointUrl}api/photos/${albumSetId}/${albumId}`).pipe(map(res => {
      return res['photos'].map(photo => new Photo(
        res['albumCollectionId'],
        res['albumId'],
        photo.id,
        photo.name
      ));
    }));
  }

  generateThumbnailPhotoUrl(albumSetId: string, albumId: string, photoId: string): string {
    return `${this.endpointUrl}api/photos/${albumSetId}/${albumId}/${photoId}/mini`;
  }
}
