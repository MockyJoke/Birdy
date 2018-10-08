import { Component, OnInit } from '@angular/core';
import { PhotoManagerService } from '../../photo-manager.service';
import { NamedType } from '../../models/named-type';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.css']
})
export class NavigatorComponent implements OnInit {

  private selectedNode: NamedType;
  constructor(private photoManagerService: PhotoManagerService) {
    this.photoManagerService.selectionChanged.subscribe(selection => {
      this.selectedNode = selection;
    });
  }

  ngOnInit() {
  }

  navigateTo(albumSetId: string, albumId: string, photoId: string) {
    this.photoManagerService.navigate(albumSetId, albumId, photoId);
  }
}
