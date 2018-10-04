import { NamedType } from './named-type';
import { AlbumSet } from './album-set';

export class Root implements NamedType {
    albumSets: AlbumSet[];

    constructor(albumSets: AlbumSet[]) {
        this.albumSets = albumSets;
    }

    getTypeName(): string {
        return "Root";
    }
}