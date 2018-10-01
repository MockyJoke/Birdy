import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from '../../../models/photo';
import { PhotoService } from '../../../photo.service';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {

  @Input() photo: Photo;
  @Output() clicked = new EventEmitter<Photo>();
  constructor(private photoService: PhotoService) { }

  ngOnInit() {
  }

  onClickMe() {
    this.clicked.emit(this.photo);
  }

  getThumbnailPhotoUrl(): string {
    return this.photoService.generateThumbnailPhotoUrl(this.photo.albumSetId, this.photo.albumId, this.photo.id);
  }
}
