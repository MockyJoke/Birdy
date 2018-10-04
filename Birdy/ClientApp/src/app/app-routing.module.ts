import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainContentComponent } from './main-content/main-content.component';
import { AlbumSetsViewerComponent } from './main-content/album-sets-viewer/album-sets-viewer.component';
import { AlbumsViewerComponent } from './main-content/albums-viewer/albums-viewer.component';
import { PhotosViewerComponent } from './main-content/photos-viewer/photos-viewer.component';

const routes: Routes = [
  { path: '', redirectTo: '/photos', pathMatch: 'full' },
  { path: 'photos', component: AlbumSetsViewerComponent },
  { path: 'photos/:albumSetId', component: AlbumsViewerComponent },
  { path: 'photos/:albumSetId/:albumId', component: PhotosViewerComponent },
  { path: 'photos/:albumSetId/:albumId/photoId', component: MainContentComponent },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
