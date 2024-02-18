Unity Project: Experimental Edge Detection and Measurement
Overview
This Unity project incorporates an experimental edge detection and measurement algorithm designed for defect quantification. It utilizes Canny parameters as a foundational basis, with modifications tailored to specific experimental needs. This README outlines the current state of the project, its capabilities, and areas pending further development.

Current Project Status
The version of the Unity project provided here represents an interim development stage. Key features and enhancements are planned for future releases to augment its functionality and accuracy. It's important for users to note the following aspects of the current release:

Incomplete Features:

The project lacks perspective correction capabilities, which are essential for accurate image analysis in varying conditions.
An automatic Region of Interest (ROI) algorithm has not been incorporated yet. Users will need to manually select the ROI for processing.
Pixel to millimeter (mm) conversion is not precisely calibrated. As such, measurements obtained from this version may not reflect exact real-world dimensions.
Algorithmic Approach:

While the project employs Canny parameters for edge detection, it does not adhere strictly to the traditional Canny edge detection algorithm. Instead, it has been modified based on personal experimentation, particularly with a horizontal approach for measurement.
These alterations aim to adapt the algorithm for specific use cases within the project, primarily focusing on defect quantification.
Foundation and Purpose:

The included algorithm serves as a basic framework initially developed for the purpose of defect quantification. It represents the project's core upon which further refinements and functionalities will be built.
Future Development
Work is ongoing to enhance the project's capabilities and address the current limitations. Planned updates include the integration of perspective correction, the implementation of an automatic ROI algorithm, and precise calibration for pixel to mm conversion. These enhancements aim to improve the accuracy and usability of the edge detection and measurement algorithm for practical applications.

Contributing
Feedback and contributions are welcome as we continue to refine and expand the project's features. If you're interested in contributing or have suggestions for improvement, please feel free to reach out or submit a pull request.

Acknowledgments
This project is the result of extensive research and experimentation during the first and second years of my PHD at the University of New Mexico under suppervision of Dr. Fernando Moreu. We appreciate the contributions and insights from my other colleagues specially John-Wesley Hanson (jack).

This README is intended to provide users and contributors with a clear understanding of the project's current state, its algorithmic foundation, and future development directions.
