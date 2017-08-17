import { Observable, Subject } from 'rxjs';

export class FileReaderUtils {
    static imageDataToBase64(imageData: Blob): Observable<string> {
        let result = new Subject<string>();

        let reader = new FileReader();

        reader.addEventListener('load', () => {
            let buffer: ArrayBuffer = reader.result;
            let imageBase64: string = FileReaderUtils.arrayBufferToBase64(buffer);
            result.next(imageBase64);
        });

        reader.addEventListener('error', () => {
            result.error('error reading image data');
        });

        reader.readAsArrayBuffer(imageData);

        return result;
    }

    static arrayBufferToBase64(buffer: ArrayBuffer): string {
        let binary = '';
        let bytes = new Uint8Array(buffer);
        for (let i = 0; i < bytes.byteLength; ++i) {
            binary += String.fromCharCode(bytes[i]);
        }
        return btoa(binary);
    }
}