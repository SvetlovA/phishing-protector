# phishing-protector

PhihsingDetector is a tool for finding anf deleting files with phishing links.
The application will track a given source directory on your local Windows computer.
Each HTML file that is added to the source directory should be moved to a given target directory with the following rule:
* HTML files that contain Hyperlink addresses that do not match the Hyperlink text itself, will be replaced so that the Hyperlink text will be used as the actual hyperlink address.

# How to use

1. Add paths to source and destination directories to appsettings.json:
```
"DataLocations": [
    {
      "SourceDirectory": "first source directory path",
      "DestinationDirectory": "first destination directory path"
    },
    {
      "SourceDirectory": "second source directory path",
      "DestinationDirectory": "second destination directory path"
    }
  ]
  ```
  2. Put files to source directories
  3. Run tool
  4. Check destination directories
