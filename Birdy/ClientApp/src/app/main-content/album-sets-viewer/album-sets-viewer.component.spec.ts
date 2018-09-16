import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumSetsViewerComponent } from './album-sets-viewer.component';

describe('AlbumSetsViewerComponent', () => {
  let component: AlbumSetsViewerComponent;
  let fixture: ComponentFixture<AlbumSetsViewerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AlbumSetsViewerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumSetsViewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
