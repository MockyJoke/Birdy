import { Component, OnInit } from '@angular/core';
import { NamedType } from '../models/named-type';
import { AlbumSet } from '../models/album-set';
import { Album } from '../models/album';

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {

  selectedNode : NamedType;

  constructor() { 
  }

  ngOnInit() {
    this.selectedNode = new Album();
    //console.log(this.selectedNode.getTypeName());
  }

}
