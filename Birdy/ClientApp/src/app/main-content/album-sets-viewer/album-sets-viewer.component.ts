import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PhotoService } from '../../photo.service';
import { AlbumSet } from '../../models/album-set';


@Component({
  selector: 'app-album-sets-viewer',
  templateUrl: './album-sets-viewer.component.html',
  styleUrls: ['./album-sets-viewer.component.css']
})
export class AlbumSetsViewerComponent implements OnInit {

  albumSets: AlbumSet[];
  @Output() selectionChanged = new EventEmitter<AlbumSet>();
  constructor(private photoService: PhotoService) { }

  ngOnInit() {
    this.photoService.getAlbumSets().subscribe(albumSets => { this.albumSets = albumSets; });
  }

  onClicked(selection: AlbumSet) {
    this.selectionChanged.emit(selection);
  }

}
