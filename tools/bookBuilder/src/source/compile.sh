FN log bold blue "Compile $SOURCE_DIR --> $FULL_SOURCE_FILE"

echo > "$FULL_SOURCE_FILE" # Start fresh combined source file

local file
while read -d '' file; do
  echo >> "$FULL_SOURCE_FILE"
  cat "$file" >> "$FULL_SOURCE_FILE"
  echo >> "$FULL_SOURCE_FILE"
  FN log light blue "- $file"
done < <( find "$SOURCE_DIR" -type f -name '*.md' -print0 | sort -z )