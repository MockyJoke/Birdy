import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainContentComponent } from './main-content/main-content.component';

const routes: Routes = [
  { path: '', redirectTo: '/photos', pathMatch: 'full' },
  { path: 'photos', component: MainContentComponent },
  { path: 'photos/:albumSetId', component: MainContentComponent },
  { path: 'photos/:albumSetId/:albumId', component: MainContentComponent },
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
