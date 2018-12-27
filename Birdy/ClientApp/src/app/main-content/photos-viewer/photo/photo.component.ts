import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { Photo } from '../../../models/photo';
import { PhotoService, ImageFetchMode } from '../../../photo.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss']
})
export class PhotoComponent implements OnInit, OnDestroy {
  @Input() photo: Photo;
  @Output() clicked = new EventEmitter<Photo>();
  shouldDestroy: Subject<boolean>;
  imageSrc: string;
  constructor(private photoService: PhotoService) {
    this.shouldDestroy = new Subject<boolean>();
  }

  ngOnInit() {
    this.loadThumbnailImage();
  }

  ngOnDestroy(): void {
    this.shouldDestroy.next(true);
  }

  onClickMe() {
    this.clicked.emit(this.photo);
  }

  private loadThumbnailImage() {
    this.photoService.getImage(this.photo.albumSetId, this.photo.albumId, this.photo.id, ImageFetchMode.MINI)
      .pipe(takeUntil(this.shouldDestroy)).subscribe(base64Image => {
        this.imageSrc = base64Image;
      });
  }

  getThumbnailPhotoUrl(): string {
    return this.photoService.generateThumbnailPhotoUrl(this.photo.albumSetId, this.photo.albumId, this.photo.id);
  }
}
