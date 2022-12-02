import { readFileSync } from 'fs';
import path from 'path'

function part1_strategy(x: string, y:string) {
    switch(y) {
        case 'X' : return 'A';
        case 'Y' : return 'B';
        default : return 'C'
    }
}

function part2_strategy(x: string, y:string) {
    switch(y) {
        case 'Y': // Draw 
            return x;
        case 'X': // Lose
            switch(x) {
                case 'A' : return 'C';
                case 'B' : return 'A';
                default : return 'B';
            } 
        default: // Win
            switch(x) {
                case 'A' : return 'B';
                case 'B' : return 'C';
                default : return 'A';
            }
    }
}

function score(x: string, y: string) {

    const shape_scores: { [label: string]: number } = {
        'A': 1,
        'B': 2,
        'C': 3
    };

    const outcome_score = x == y ? 3 
        : (x == 'A' && y == 'B' || x == 'B' && y == 'C' || x == 'C' && y == 'A') ? 6 : 0; 

    return outcome_score + shape_scores[y];
}

function total_score(input:string[][], strategy:CallableFunction) {
    return input.map(x => [x[0], strategy(x[0],x[1])])
    .map(x => score(x[0], x[1]))
    .reduce((a,c) => a + c);
}

const fileName = path.resolve(__dirname, process.argv[2])
const file = readFileSync(fileName, 'utf-8');
const input = file.split("\r\n").map(x => x.split(' '));
console.log(`Part One: ${total_score(input, part1_strategy)}`);
console.log(`Part Two: ${total_score(input, part2_strategy)}`);