import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPhotoViewerComponent } from './main-photo-viewer.component';

describe('MainPhotoViewerComponent', () => {
  let component: MainPhotoViewerComponent;
  let fixture: ComponentFixture<MainPhotoViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainPhotoViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainPhotoViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
