import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MainContentComponent } from './main-content/main-content.component';
import { AlbumSetsViewerComponent } from './main-content/album-sets-viewer/album-sets-viewer.component';
import { PhotosViewerComponent } from './main-content/photos-viewer/photos-viewer.component';
import { AlbumsViewerComponent } from './main-content/albums-viewer/albums-viewer.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MainContentComponent,
    AlbumSetsViewerComponent,
    AlbumsViewerComponent,
    PhotosViewerComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
