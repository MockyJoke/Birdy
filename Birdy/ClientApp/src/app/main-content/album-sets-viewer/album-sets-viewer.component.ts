import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PhotoService } from '../../photo.service';
import { AlbumSet } from '../../models/album-set';
import { PhotoManagerService } from '../../photo-manager.service';
import { Root } from '../../models/root';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-album-sets-viewer',
  templateUrl: './album-sets-viewer.component.html',
  styleUrls: ['./album-sets-viewer.component.scss']
})
export class AlbumSetsViewerComponent implements OnInit {

  root: Root;
  @Output() selectionChanged = new EventEmitter<AlbumSet>();
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.photoManagerService.navigateByRoute(this.route.snapshot).subscribe(selection => {
      if (selection.getTypeName() === "Root") {
        this.root = selection as Root;
      }
    });
  }

  onClicked(selection: AlbumSet) {
    this.selectionChanged.emit(selection);
    this.photoManagerService.navigateByNode(selection);
  }
}
