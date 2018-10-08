import { Injectable, EventEmitter } from '@angular/core';
import { NamedType } from './models/named-type';
import { Subject, Observable } from 'rxjs';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlbumSet } from './models/album-set';
import { Album } from './models/album';
import { Photo } from './models/photo';
import { PhotoService } from './photo.service';
import { Root } from './models/root';
import { map } from 'rxjs/operators';

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

  navigateByNode(selectedNode: NamedType) {
    var selectionTypeName = selectedNode.getTypeName();
    if (selectionTypeName === "AlbumSet") {
      var albumSet = selectedNode as AlbumSet;
      this.navigate(albumSet.id, null, null);
    }
    else if (selectionTypeName === "Album") {
      var album = selectedNode as Album;
      this.navigate(album.albumSetId, album.id, null);
    }
    else if (selectionTypeName === "Photo") {
      var photo = selectedNode as Photo;
      this.navigate(photo.albumSetId, photo.albumId, photo.id);
    }
    else {
      this.navigate(null, null, null);
    }
  }

  navigate(albumSetId: string, albumId: string, photoId: string) {
    if (albumSetId != null && albumId != null && photoId != null) {
      this.router.navigateByUrl(`/photos/${albumSetId}/${albumId}/${photoId}`);
    }
    else if (albumSetId != null && albumId != null) {
      this.router.navigateByUrl(`/photos/${albumSetId}/${albumId}`);
    }
    else if (albumSetId != null) {
      this.router.navigateByUrl(`/photos/${albumSetId}`);
    }
    else {
      this.router.navigateByUrl(`/photos`);
    }
  }

  navigateByRoute(routeSnapshot: ActivatedRouteSnapshot): Observable<NamedType> {
    var albumSetId = routeSnapshot.paramMap.get('albumSetId');
    var albumId = routeSnapshot.paramMap.get('albumId');
    var photoId = routeSnapshot.paramMap.get('photoId');
    var result: Observable<NamedType>;
    if (albumSetId != null && albumId != null && photoId != null) {
      //do something
    }
    else if (albumSetId != null && albumId != null) {
      result = this.photoService.getAlbum(albumSetId, albumId);
    }
    else if (albumSetId != null) {
      result = this.photoService.getAlbumSet(albumSetId);
    } else {
      result = this.photoService.getRoot();
    }
    return result.pipe(map(node => {
      this.selectedNode = node;
      this.selectionChangedSource.emit(node)
      return node;
    }));
  }
}
