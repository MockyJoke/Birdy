import { Component, OnInit, Output, EventEmitter, Input, OnChanges, QueryList, AfterViewInit, AfterContentInit, AfterViewChecked, ViewChildren } from '@angular/core';
import { AlbumSet } from '../../models/album-set';
import { Album } from '../../models/album';
import { PhotoManagerService } from '../../photo-manager.service';
import { ActivatedRoute } from '@angular/router';
import { AlbumComponent } from './album/album.component';
import { concat } from 'rxjs';

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
    if (this.app_albums != null) {
      this.app_albums.changes.subscribe(async app_albums => {
        var app_albums_array = app_albums.toArray();
        for (let i = 0; i < app_albums_array.length; i++) {
          await app_albums_array[i].updatePreview().toPromise();
        }
      });
    }
  }

  onClicked(selection: Album) {
    this.selectionChanged.emit(selection);
    this.photoManagerService.navigateByNode(selection);
  }

  previewAlbums() {
    concat(this.app_albums.map(a => a.updatePreview())).subscribe(album => { });
  }
}
