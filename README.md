# phishing-protector

PhihsingDetector is a tool for finding anf deleting files with phishing links.
The application will track a given source directory on your local Windows computer.
Each HTML file that is added to the source directory should be moved to a given target directory with the following rule:
* HTML files that contain Hyperlink addresses that do not match the Hyperlink text itself, will be replaced so that the Hyperlink text will be used as the actual hyperlink address.
