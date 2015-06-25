#! /usr/lical/bin/python
import os,Image,sys
from xml.etree import ElementTree

def slice(image, x, y, size):
	box = (x * size, y * size, (x + 1)*size, (y + 1)*size)
	return image.crop(box)

if __name__ == '__main__':
    path = sys.argv[1]
    size = int(sys.argv[2])
    
    if not os.path.exists(path):
        print "make sure you have boith plist and png files in the same directory"
    
    image = Image.open(path)
    
    gen_path = os.path.splitext(os.path.split(path)[1])[0]
    if not os.path.isdir(gen_path):
        os.mkdir(gen_path)
    
    width = image.size[0]
    height = image.size[1]
    tile_width = width/size
    tile_height = height/size
    
    for i in range(0, tile_width * tile_height):
        slice(image, i%tile_width, i/tile_height, size).save(gen_path + "/" + str(i) + ".png")
