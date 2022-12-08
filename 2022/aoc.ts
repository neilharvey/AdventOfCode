import { readFileSync } from 'fs';
import path from 'path'

export function readLines() {
    const scriptDirectory = path.dirname(process.argv[1]);
    const fileName = path.resolve(scriptDirectory, process.argv[2])
    const file = readFileSync(fileName, 'utf-8');
    return file.split("\r\n");
}

export function readMatrix() {
    let matrix: Array<Array<number>> = [];
    const input = readLines();
    for (let row = 0; row < input.length; row++) {
        matrix.push(new Array<number>());
        for (let col = 0; col < input.length; col++) {
            matrix[row].push(Number(input[row][col]));
        }
    }

    return matrix;
}