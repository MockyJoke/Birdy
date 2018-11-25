import { Component, OnInit, Output, EventEmitter, Input, OnChanges, QueryList, AfterViewInit, AfterContentInit, AfterViewChecked, ViewChildren } from '@angular/core';
import { AlbumSet } from '../../models/album-set';
import { Album } from '../../models/album';
import { PhotoManagerService } from '../../photo-manager.service';
import { ActivatedRoute } from '@angular/router';
import { AlbumComponent } from './album/album.component';

@Component({
  selector: 'app-albums-viewer',
  templateUrl: './albums-viewer.component.html',
  styleUrls: ['./albums-viewer.component.css']
})
export class AlbumsViewerComponent implements OnInit, AfterViewInit {
  @Input() albumSet: AlbumSet;
  @Output() selectionChanged = new EventEmitter<Album>();
  @ViewChildren('app_albums') app_albums: QueryList<AlbumComponent>;
  constructor(private photoManagerService: PhotoManagerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.photoManagerService.navigateByRoute(this.route.snapshot).subscribe(selection => {
      if (selection.getTypeName() === "AlbumSet") {
        this.albumSet = selection as AlbumSet;
      }
    });

  }
  ngAfterViewInit(): void {
  }

  onClicked(selection: Album) {
    this.selectionChanged.emit(selection);
    this.photoManagerService.navigateByNode(selection);
  }
}
