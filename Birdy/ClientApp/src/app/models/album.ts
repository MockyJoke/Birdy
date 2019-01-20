import { Photo } from './photo'
import { NamedType } from './named-type';

export class Album implements NamedType {
    albumSetId: string;
    albumSetName: string;
    id: string;
    name: string;
    private photos: Photo[];
    private previewImages: string[];

    constructor(albumSetId: string, albumSetName: string, id: string, name: string) {
        this.albumSetId = albumSetId;
        this.albumSetName = albumSetName;
        this.id = id;
        this.name = name;
    }

    getTypeName(): string {
        return "Album";
    }

    getPhotos(): Photo[] {
        return this.photos;
    }

    getPrewviewImages(): string[] {
        return this.previewImages;
    }

    setPhotos(photos: Photo[]) {
        this.photos = photos;
    }

    setPrewviewImages(previewImages: string[]) {
        this.previewImages = previewImages;
    }
}