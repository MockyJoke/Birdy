import { Photo } from './photo'
import { NamedType } from './named-type';

export class Album implements NamedType {
    albumSetId: string;
    albumSetName: string;
    id: string;
    name: string;
    private photos: Photo[];

    constructor(albumSetId: string, albumSetName: string, id: string, name: string) {
        this.albumSetId = albumSetId;
        this.albumSetName = albumSetName;
        this.id = id;
        this.name = name;
    }

    getPhotos(): Photo[] {
        return this.photos;
    }

    getTypeName(): string {
        return "Album";
    }

    setPhotos(photos: Photo[]) {
        this.photos = photos;
    }
}