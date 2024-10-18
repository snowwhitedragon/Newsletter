import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
  })
export class CommonService {

    constructor() {}

    /**
     * Converts a byte array to base64 string to display as image
     * @param byteArray The array to convert.
     * @returns the source for an image.
     */
    public convertByteArrayToBase64(byteArray: Uint8Array): Promise<string> {
        return new Promise((resolve, reject) => {
            try {
                // Create a blob from the byte array
                const blob = new Blob([byteArray], { type: 'image/jpeg' });  // Adjust the type as per your file
                const reader = new FileReader();
            
                // Listen for the load event to get the base64
                reader.onloadend = () => {
                    resolve(reader.result as string);
                };
            
                // Trigger reading the blob as DataURL (Base64)
                reader.readAsDataURL(blob);
            } catch (error) {
                reject(error);
            }
        });
    }

    /**
     * Converts a File object to a Uint8Array (byte array).
     * @param file The file to convert.
     * @returns A Promise that resolves to the byte array (Uint8Array).
     */
    public fileToByteArray(file: File): Promise<Uint8Array> {
        return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = (event: any) => {
            const arrayBuffer = event.target.result;
            const byteArray = new Uint8Array(arrayBuffer);
            resolve(byteArray);
        };

        reader.onerror = (error) => {
            reject(error);
        };

        reader.readAsArrayBuffer(file);
        });
    }
}