import { readInput } from '../aoc';

const directions: [number, number][] = [[1, 0], [-1, 0], [0, 1], [0, -1]];

function readMap() {

    let map: Array<Array<number>> = [];
    let input = readInput();
    for (let row = 0; row < input.length; row++) {
        map.push(new Array<number>());
        for (let col = 0; col < input.length; col++) {
            map[row].push(Number(input[row][col]));
        }
    }

    return map;
}

function isVisibleInDirection(map: Array<Array<number>>, position: [number, number], direction: [number, number]) {

    let height = map[position[0]][position[1]];
    let i = 1;
    let x = position[0] + direction[0];
    let y = position[1] + direction[1];

    do {
        if (map[x][y] >= height) {
            return false;
        }

        i++;
        x = position[0] + (i * direction[0]);
        y = position[1] + (i * direction[1]);
    }
    while (x >= 0 && x < map.length && y >= 0 && y < map.length);

    return true;
}

function getVisibleTrees(map: Array<Array<number>>) {

    let rows = map.length;
    let cols = map[0].length;
    let visible = ((rows + cols) * 2) - 4;
    for (let row = 1; row < rows - 1; row++) {
        for (let col = 1; col < cols - 1; col++) {
            for (let d = 0; d < directions.length; d++) {
                if (isVisibleInDirection(map, [row, col], directions[d])) {
                    visible++;
                    break;
                }
            }
        }
    }

    return visible;
}

function getViewingDistance(map: Array<Array<number>>, position: [number, number], direction: [number, number]) {

    let height = map[position[0]][position[1]];
    let distance = 1;
    let x = position[0] + direction[0];
    let y = position[1] + direction[1];

    while (x >= 0 && x < map.length && y >= 0 && y < map.length) {
        if (map[x][y] >= height) {
            return distance;
        }

        distance++;
        x = position[0] + (distance * direction[0]);
        y = position[1] + (distance * direction[1]);
    }

    return distance - 1;
}

function getHighestScenicScore(map: Array<Array<number>>) {

    let rows = map.length;
    let cols = map[0].length;
    let best = 0;
    for (let row = 1; row < rows - 1; row++) {
        for (let col = 1; col < cols - 1; col++) {

            let viewingDistances: number[] = [];
            directions.forEach(direction => {
                let distance = getViewingDistance(map, [row,col], direction);
                viewingDistances.push(distance);
            });
        
            let score = viewingDistances.reduce((a, c) => a * c, 1);
            best = Math.max(best, score);
        }
    }

    return best;
}

const map = readMap();
const visible = getVisibleTrees(map);
console.log(`Part One: ${visible}`);
const score = getHighestScenicScore(map);
console.log(`Part Two: ${score}`);