import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { PhotoService } from '../../photo.service';
import { Album } from '../../models/album';
import { ActivatedRoute } from '@angular/router';
import { PhotoManagerService } from '../../photo-manager.service';
import { Photo } from '../../models/photo';

@Component({
  selector: 'app-photos-viewer',
  templateUrl: './photos-viewer.component.html',
  styleUrls: ['./photos-viewer.component.css']
})
export class PhotosViewerComponent implements OnInit {

  @Input() album: Album;
  @Output() selectionChanged = new EventEmitter<Photo>();
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.photoManagerService.navigateByRoute(this.route.snapshot).subscribe(selection => {
      if (selection.getTypeName() === "Album") {
        this.album = selection as Album;
        this.photoManagerService.navigateByNode(selection);
      }
    });
  }
  onClicked(selection: Photo) {
    this.selectionChanged.emit(selection);
    this.photoManagerService.navigateByNode(selection);
  }
}
