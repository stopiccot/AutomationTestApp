#import <UIKit/UIKit.h>

extern "C" {

	// Returns device screen scale factor
    float _scaleFactor() {
        return [[UIScreen mainScreen] scale];
    }

    // Returns status bar height in pixels
    int _statusBarHeightInPixels() {
    	return [UIApplication sharedApplication].statusBarFrame.size.height * _scaleFactor();
    }

}
