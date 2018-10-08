import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AlbumSet } from '../../models/album-set';
import { Album } from '../../models/album';
import { PhotoManagerService } from '../../photo-manager.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-albums-viewer',
  templateUrl: './albums-viewer.component.html',
  styleUrls: ['./albums-viewer.component.css']
})
export class AlbumsViewerComponent implements OnInit {

  @Input() albumSet: AlbumSet;
  @Output() selectionChanged = new EventEmitter<Album>();
  constructor(private photoManagerService: PhotoManagerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.photoManagerService.navigateByRoute(this.route.snapshot).subscribe(selection => {
      if (selection.getTypeName() === "AlbumSet") {
        this.albumSet = selection as AlbumSet;
      }
    });
  }

  onClicked(selection: Album) {
    this.selectionChanged.emit(selection);
    this.photoManagerService.navigateByNode(selection);
  }
}
