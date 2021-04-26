FN log light cyan "- Updating installed $PDF_LATEX_TEMPLATE_NAME template"


mkdir -p "$PDF_PANDOC_TEMPLATE_DIRECTORY"
if [ -n "$pdfLatexFile" ]
then
  cp "$pdfLatexFile" "$PDF_PANDOC_TEMPLATE_DIRECTORY/"
else
  cp "$PDF_LATEX_SIMPLE_TEMPLATE_FILE" "$PDF_PANDOC_TEMPLATE_DIRECTORY/"
fi