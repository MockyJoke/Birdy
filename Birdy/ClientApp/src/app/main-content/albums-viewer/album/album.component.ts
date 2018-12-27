import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { Album } from '../../../models/album';
import { PhotoService, ImageFetchMode } from 'src/app/photo.service';
import { Photo } from 'src/app/models/photo';
import { Observable, ObjectUnsubscribedError, concat, merge, forkJoin, Subject } from 'rxjs';
import { tap, switchMap, take, map, flatMap, mergeMap, bufferCount, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.scss']
})
export class AlbumComponent implements OnInit, OnDestroy {

  @Input() album: Album;
  @Output() clicked = new EventEmitter<Album>();
  shouldLoad: boolean;
  previewPhotos: Photo[];
  previewSrcs: string[];
  shouldDestroy: Subject<boolean>;
  constructor(private photoService: PhotoService) {
    this.previewSrcs = new Array<string>();
    this.shouldDestroy = new Subject<boolean>();
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.shouldDestroy.next(true);
  }

  onClickMe() {
    this.clicked.emit(this.album);
  }

  public fillThumbnailUrl(): Observable<Album> {
    var album_observable = this.photoService.getAlbum(this.album.albumSetId, this.album.id)
      .pipe(tap(album => {
        this.previewPhotos = album.getPhotos().slice(0, 4);
        this.previewSrcs = album.getPhotos().slice(0, 4).map(p => this.photoService.generateThumbnailPhotoUrl(p.albumSetId, p.albumId, p.id));
      }));
    return album_observable;
  }

  public loadPreviewImages() {
    var album_observable = this.photoService.getAlbum(this.album.albumSetId, this.album.id)
      .pipe(switchMap(album => {
        this.previewPhotos = album.getPhotos().slice(0, 4);
        var obss = this.previewPhotos.map(p => this.photoService.getImage(p.albumSetId, p.albumId, p.id, ImageFetchMode.MINI));
        return merge(...obss).pipe(take(this.previewPhotos.length));
      }), takeUntil(this.shouldDestroy));
    return album_observable.pipe(tap(src => {
      this.previewSrcs.push(src);
    }));
  }
}
