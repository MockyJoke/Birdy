import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Album } from '../../../models/album';
import { PhotoManagerService } from '../../../photo-manager.service';
import { PhotoService } from '../../../photo.service';
import { Photo } from '../../../models/photo';

@Component({
  selector: 'app-photo-list',
  templateUrl: './photo-list.component.html',
  styleUrls: ['./photo-list.component.css']
})
export class PhotoListComponent implements OnInit {

  @Input() album: Album;
  @Output() clicked = new EventEmitter<Photo>();
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService) { }

  ngOnInit() {
  }

  onClickMe(photoId: string) {
    this.clicked.emit(this.album.getPhotos().filter(p => p.id === photoId)[0]);
  }

}
