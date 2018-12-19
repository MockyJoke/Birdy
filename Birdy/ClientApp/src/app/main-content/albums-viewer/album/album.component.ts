import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Album } from '../../../models/album';
import { PhotoService } from 'src/app/photo.service';
import { Photo } from 'src/app/models/photo';
import { Observable, ObjectUnsubscribedError, concat, merge, forkJoin } from 'rxjs';
import { tap, switchMap, take, map, flatMap, mergeMap, bufferCount } from 'rxjs/operators';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.scss']
})
export class AlbumComponent implements OnInit {

  @Input() album: Album;
  @Output() clicked = new EventEmitter<Album>();
  shouldLoad: boolean;
  previewPhotos: Photo[]; // To be removed
  previewSrcs: string[];
  constructor(private photoService: PhotoService) {
    this.previewSrcs = new Array<string>();
  }

  ngOnInit() {
  }

  onClickMe() {
    this.clicked.emit(this.album);
  }

  public updatePreview(): Observable<string> {
    var album_observable = this.photoService.getAlbum(this.album.albumSetId, this.album.id)
      .pipe(switchMap(album => {
        this.previewPhotos = album.getPhotos().slice(0, 4);
        var obss = this.previewPhotos.map(p => this.preloadImage(this.photoService.generateThumbnailPhotoUrl(p.albumSetId, p.albumId, p.id)));
        return merge(...obss).pipe(take(this.previewPhotos.length));
      }));
    return album_observable.pipe(tap(src => {
      this.previewSrcs.push(src);
    }));
  }

  public fillThumbnailUrl(): Observable<Album> {
    var album_observable = this.photoService.getAlbum(this.album.albumSetId, this.album.id)
      .pipe(tap(album => {
        this.previewPhotos = album.getPhotos().slice(0, 4);
        this.previewSrcs = album.getPhotos().slice(0, 4).map(p => this.photoService.generateThumbnailPhotoUrl(p.albumSetId, p.albumId, p.id));
      }));
    return album_observable;
  }

  preloadImage(url: string): Observable<string> {
    return Observable.create(observer => {
      var image = new Image();
      image.onload = function () {
        observer.next(image.src);
      };
      image.src = url;
    });
  }
}
