import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Album } from '../../../models/album';

@Component({
  selector: 'app-album',
  templateUrl: './album.component.html',
  styleUrls: ['./album.component.css']
})
export class AlbumComponent implements OnInit {

  @Input() album : Album;
  @Output() clicked = new EventEmitter<Album>();
  constructor() { }

  ngOnInit() {
  }

  onClickMe() {
    this.clicked.emit(this.album);
  }

}
