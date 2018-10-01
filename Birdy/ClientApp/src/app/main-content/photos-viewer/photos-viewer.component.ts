import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Photo } from '../../models/photo';
import { PhotoService } from '../../photo.service';
import { Album } from '../../models/album';

@Component({
  selector: 'app-photos-viewer',
  templateUrl: './photos-viewer.component.html',
  styleUrls: ['./photos-viewer.component.css']
})
export class PhotosViewerComponent implements OnInit, OnChanges {


  @Input() album: Album;
  photos: Photo[];
  constructor(private photoService: PhotoService) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.photoService.getPhotos(this.album.albumSetId, this.album.id).subscribe(photos => {
      this.photos = photos;
    });
  }

}
