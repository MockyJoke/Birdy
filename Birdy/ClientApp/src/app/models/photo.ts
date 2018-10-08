import { NamedType } from "./named-type"

export class Photo implements NamedType {
    albumSetId: string;
    albumSetName: string;
    albumId: string;
    albumName: string;
    id: string;
    name: string;

    constructor(albumSetId: string, albumSetName: string, albumId: string, albumName: string, id: string, name: string) {
        this.albumSetId = albumSetId;
        this.albumName = albumName;
        this.albumId = albumId;
        this.albumName = albumName;
        this.id = id;
        this.name = name;
    }
    getTypeName(): string {
        return "Photo";
    }
}