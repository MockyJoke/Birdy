import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotosViewerComponent } from './photos-viewer.component';

describe('PhotosViewerComponent', () => {
  let component: PhotosViewerComponent;
  let fixture: ComponentFixture<PhotosViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhotosViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotosViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
