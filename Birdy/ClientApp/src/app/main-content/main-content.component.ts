import { Component, OnInit, Input } from '@angular/core';
import { NamedType } from '../models/named-type';
import { AlbumSet } from '../models/album-set';
import { Album } from '../models/album';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../photo.service';
import { Photo } from '../models/photo';
import { Location } from '@angular/common'

@Component({
  selector: 'app-main-content',
  templateUrl: './main-content.component.html',
  styleUrls: ['./main-content.component.css']
})
export class MainContentComponent implements OnInit {

  selectedNode: NamedType;

  constructor(private photoService: PhotoService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit() {
    var albumSetId = this.route.snapshot.paramMap.get('albumSetId');
    var albumId = this.route.snapshot.paramMap.get('albumId');
    var photoId = this.route.snapshot.paramMap.get('photoId');

    if (albumSetId != null && albumId != null) {
      this.photoService.getAlbum(albumSetId, albumId).subscribe(album => {
        this.selectedNode = album;
      });
      return
    }

    if (albumSetId != null) {
      this.photoService.getAlbumSet(albumSetId).subscribe(albumSet => {
        this.selectedNode = albumSet;
      });
      return
    }
  }

  onSelectionChanged(selection: NamedType) {
    var selectionTypeName = selection.getTypeName();
    if (selectionTypeName === "AlbumSet") {
      var albumSet = selection as AlbumSet;
      this.router.navigateByUrl(`/photos/${albumSet.id}`);
    }
    else if (selectionTypeName === "Album") {
      var album = selection as Album;
      this.router.navigateByUrl(`/photos/${album.albumSetId}/${album.id}`);
    }
    else if (selectionTypeName === "Photo") {
      var photo = selection as Photo;
      this.router.navigateByUrl(`/photos/${photo.albumSetId}/${photo.id}/${photo.id}`);
    }
    this.selectedNode = selection;
  }

}
