import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; 

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MainContentComponent } from './main-content/main-content.component';
import { AlbumSetsViewerComponent } from './main-content/album-sets-viewer/album-sets-viewer.component';
import { PhotosViewerComponent } from './main-content/photos-viewer/photos-viewer.component';
import { AlbumsViewerComponent } from './main-content/albums-viewer/albums-viewer.component';
import { AlbumSetComponent } from './main-content/album-sets-viewer/album-set/album-set.component';
import { AlbumComponent } from './main-content/albums-viewer/album/album.component';
import { PhotoComponent } from './main-content/photos-viewer/photo/photo.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MainContentComponent,
    AlbumSetsViewerComponent,
    AlbumsViewerComponent,
    PhotosViewerComponent,
    AlbumSetComponent,
    AlbumComponent,
    PhotoComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
