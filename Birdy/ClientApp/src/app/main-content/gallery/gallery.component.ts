import { Component, OnInit, Input } from '@angular/core';
import { PhotoManagerService } from '../../photo-manager.service';
import { ActivatedRoute } from '@angular/router';
import { Photo } from '../../models/photo';
import { PhotoService } from '../../photo.service';
import { Album } from '../../models/album';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {

  @Input() photo: Photo;
  album: Album;
  constructor(private photoService: PhotoService, private photoManagerService: PhotoManagerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.photoManagerService.navigateByRoute(this.route.snapshot).pipe(switchMap(selection => {
      this.photo = selection as Photo;
      this.photoManagerService.navigateByNode(selection);
      return this.photoService.getAlbum(this.photo.albumSetId, this.photo.albumId);
    })).
      subscribe(album => {
        this.album = album;
      });
  }

  getHdPhotoUrl(): string {
    return this.photoService.generateHdPhotoUrl(this.photo.albumSetId, this.photo.albumId, this.photo.id);
  }

  onClicked(selection: Photo) {
    // this.selectionChanged.emit(selection);
    this.photo = selection;
  }
}
