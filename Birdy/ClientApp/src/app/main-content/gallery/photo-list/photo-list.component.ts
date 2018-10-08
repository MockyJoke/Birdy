import { Component, OnInit, Input } from '@angular/core';
import { Album } from '../../../models/album';
import { PhotoManagerService } from '../../../photo-manager.service';
import { PhotoService } from '../../../photo.service';

@Component({
  selector: 'app-photo-list',
  templateUrl: './photo-list.component.html',
  styleUrls: ['./photo-list.component.css']
})
export class PhotoListComponent implements OnInit {

  @Input() album: Album;
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService) { }

  ngOnInit() {
  }

}
