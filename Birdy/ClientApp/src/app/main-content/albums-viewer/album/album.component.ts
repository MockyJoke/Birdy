import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Album } from '../../../models/album';
import { PhotoService } from 'src/app/photo.service';
import { Photo } from 'src/app/models/photo';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  @Input() album: Album;
  @Output() clicked = new EventEmitter<Album>();

  previewPhotos: Photo[];
  constructor(private photoService: PhotoService) {  }

  ngOnInit() {
    this.photoService.getAlbum(this.album.albumSetId, this.album.id).subscribe(album => {
      this.previewPhotos = album.getPhotos().slice(0, 4);
    });
  }

  onClickMe() {
    this.clicked.emit(this.album);
  }

}
