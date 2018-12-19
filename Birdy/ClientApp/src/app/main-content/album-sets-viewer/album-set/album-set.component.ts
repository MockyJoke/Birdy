import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AlbumSet } from '../../../models/album-set';

@Component({
  selector: 'app-album-set',
  templateUrl: './album-set.component.html',
  styleUrls: ['./album-set.component.scss']
})
export class AlbumSetComponent implements OnInit {

  @Input() albumSet: AlbumSet;
  @Output() clicked = new EventEmitter<AlbumSet>();
  constructor() { }

  ngOnInit() {
  }

  onClickMe() {
    this.clicked.emit(this.albumSet);
  }
}
