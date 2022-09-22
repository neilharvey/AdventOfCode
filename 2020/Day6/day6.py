import sys

path = sys.argv[1]
with open(path) as f:
   input = f.read().splitlines()

any_question_answered = 0
all_question_answered = 0

current_any_group = set()
current_all_group = set()

for line in input:
    if line:
        if len(current_any_group) == 0:
            current_all_group = set(line)
        for answer in line:
            current_any_group.add(answer)
            current_all_group = current_all_group.intersection(set(line))
    else:
        any_question_answered += len(current_any_group)
        all_question_answered += len(current_all_group)
        current_any_group = set()
        current_all_group = set()

any_question_answered += len(current_any_group)
all_question_answered += len(current_all_group)

print(f"Part One: {any_question_answered}")
print(f"Part Two: {all_question_answered}")