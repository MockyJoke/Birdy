import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PhotoService } from '../../photo.service';
import { Album } from '../../models/album';
import { ActivatedRoute } from '@angular/router';
import { PhotoManagerService } from '../../photo-manager.service';

@Component({
  selector: 'app-photos-viewer',
  templateUrl: './photos-viewer.component.html',
  styleUrls: ['./photos-viewer.component.css']
})
export class PhotosViewerComponent implements OnInit {

  @Input() album: Album;
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.photoManagerService.getCurrentSelection(this.route.snapshot).subscribe(selection => {
      if (selection.getTypeName() === "Album") {
        this.album = selection as Album;
      }
    });
  }
}
