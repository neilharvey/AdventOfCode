import { readFileSync } from 'fs';
import path from 'path'
const fileName = path.resolve(__dirname, process.argv[2])
const file = readFileSync(fileName, 'utf-8');
const lines = file.split("\r\n");

function get_priority(item:string) {
    const lower_a = 97;
    const upper_a = 65;
    const char_code = item.charCodeAt(0);
    return char_code >= lower_a ? 1 + char_code - lower_a : 27 + char_code - upper_a;    
}

function get_common_item(bags:Set<string>[]) {
    return bags
        .reduce((first, second) => new Set([...first].filter(x => second.has(x))))
        .values()
        .next()
        .value;
}

let part1 = 0;

lines.forEach(line => {
    const common_item = get_common_item([new Set(line.substring(0, line.length / 2)), new Set(line.substring(line.length / 2))]);
    part1 += get_priority(common_item);
});

console.log(`Part One: ${part1}`);

let part2 = 0;

for(let i = 0; i < lines.length; i += 3){
    const common_item = get_common_item([new Set(lines[i]), new Set(lines[i+1]), new Set(lines[i+2])]);
    part2 += get_priority(common_item);
}

console.log(`Part Two: ${part2}`);