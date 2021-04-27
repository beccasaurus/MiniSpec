
FN log bold green "Generating ePub"

cp "$EPUB_FULL_SOURCE_HEADER_FILE" "$EPUB_FULL_SOURCE_FILE"
echo >> "$EPUB_FULL_SOURCE_FILE"
cat "$FULL_SOURCE_FILE" >> "$EPUB_FULL_SOURCE_FILE"

pandoc                                    \
  --css "$EPUB_CSS_FILE"                  \
  --toc                                   \
  --toc-depth=3                           \
  --listings                              \
  --top-level-division=chapter            \
  --highlight-style pygments              \
  -H "$PDF_LATEX_PREAMBLE_FILE"           \
  -V classoption=oneside                  \
  -V linkcolor:blue                       \
  -f markdown+raw_tex                     \
  -o "$EPUB_OUTPUT_FILE"                   \
  "$EPUB_FULL_SOURCE_FILE"
