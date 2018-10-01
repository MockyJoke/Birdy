import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { PhotoService } from '../../photo.service';
import { AlbumSet } from '../../models/album-set';
import { Album } from '../../models/album';

@Component({
  selector: 'app-albums-viewer',
  templateUrl: './albums-viewer.component.html',
  styleUrls: ['./albums-viewer.component.css']
})
export class AlbumsViewerComponent implements OnInit, OnChanges {


  @Input() albumSet: AlbumSet;
  albums: Album[];
  @Output() selectionChanged = new EventEmitter<Album>();
  constructor(private photoService: PhotoService) { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.photoService.getAlbums(this.albumSet.id).subscribe(albums => {
      this.albums = albums;
    });
  }

  onClicked(selection: Album) {
    this.selectionChanged.emit(selection);
  }
}
