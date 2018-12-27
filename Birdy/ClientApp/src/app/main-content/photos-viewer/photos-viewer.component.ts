import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Album } from '../../models/album';
import { Photo } from '../../models/photo';
import { PhotoManagerService } from '../../photo-manager.service';
import { PhotoService } from '../../photo.service';

@Component({
  selector: 'app-photos-viewer',
  templateUrl: './photos-viewer.component.html',
  styleUrls: ['./photos-viewer.component.scss']
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
