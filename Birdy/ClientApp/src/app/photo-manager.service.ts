import { Injectable, EventEmitter } from '@angular/core';
import { NamedType } from './models/named-type';
import { Subject, Observable } from 'rxjs';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlbumSet } from './models/album-set';
import { Album } from './models/album';
import { Photo } from './models/photo';
import { PhotoService } from './photo.service';

@Injectable({
  providedIn: 'root'
})
export class PhotoManagerService {

  private selectedNode: NamedType;
  private selectionChangedSource = new EventEmitter<NamedType>();

  selectionChanged = this.selectionChangedSource.asObservable();

  constructor(
    private photoService: PhotoService,
    private router: Router) { }

  selectedNewNode(selectedNode: NamedType) {
    this.selectedNode = selectedNode;
    this.selectionChangedSource.emit(selectedNode);

    var selectionTypeName = selectedNode.getTypeName();
    if (selectionTypeName === "AlbumSet") {
      var albumSet = selectedNode as AlbumSet;
      this.router.navigateByUrl(`/photos/${albumSet.id}`);
    }
    else if (selectionTypeName === "Album") {
      var album = selectedNode as Album;
      this.router.navigateByUrl(`/photos/${album.albumSetId}/${album.id}`);
    }
    else if (selectionTypeName === "Photo") {
      var photo = selectedNode as Photo;
      this.router.navigateByUrl(`/photos/${photo.albumSetId}/${photo.id}/${photo.id}`);
    }
  }

  getCurrentSelection(routeSnapshot: ActivatedRouteSnapshot): Observable<NamedType> {
    var albumSetId = routeSnapshot.paramMap.get('albumSetId');
    var albumId = routeSnapshot.paramMap.get('albumId');
    var photoId = routeSnapshot.paramMap.get('photoId');
    if (albumSetId != null && albumId != null) {
      return this.photoService.getAlbum(albumSetId, albumId);
    }

    if (albumSetId != null) {
      return this.photoService.getAlbumSet(albumSetId);
    }
    return this.photoService.getRoot();
  }
}
