import { Component, OnInit, Input } from '@angular/core';
import { PhotoService } from '../../../photo.service';
import { Photo } from '../../../models/photo';

@Component({
  selector: 'app-main-photo-viewer',
  templateUrl: './main-photo-viewer.component.html',
  styleUrls: ['./main-photo-viewer.component.scss']
})
export class MainPhotoViewerComponent implements OnInit {

  @Input() photo: Photo;
  constructor(private photoService: PhotoService) { }

  ngOnInit() {
  }

}
