#! /usr/lical/bin/python
import os,Image,sys
from xml.etree import ElementTree

def gen_png_from_plist(xml_filename, png_filename, file_path):
    big_image = Image.open(png_filename)
    root = ElementTree.fromstring(open(xml_filename, 'r').read())
    for child in root:
        region = TextureRegion(child)
        rectlist = [region.x, region.y, region.width, region.height]
        width = int( rectlist[3] if region.rotated else rectlist[2] )
        height = int( rectlist[2] if region.rotated else rectlist[3] )
        box=( 
            int(rectlist[0]),
            int(rectlist[1]),
            int(rectlist[0]) + width,
            int(rectlist[1]) + height,
            )
        sizelist = [ region.srcwidth, region.srcheight ]
        rect_on_big = big_image.crop(box)
        result_image = Image.new('RGBA', sizelist, (0,0,0,0))
        result_box=(
            ( sizelist[0] - width )/2,
            ( sizelist[1] - height )/2,
            ( sizelist[0] + width )/2,
            ( sizelist[1] + height )/2
            )
        result_image.paste(rect_on_big, result_box, mask=0)
        if region.rotated:
            result_image = result_image.rotate(90)
        outfile = (file_path + '/' + region.src)
        print outfile, "generated"
        result_image.save(outfile)

if __name__ == '__main__':
    png_filename = sys.argv[1]
    xml_filename = sys.argv[2]
    if (os.path.exists(xml_filename) and os.path.exists(png_filename)):
        file_path = xml_filename.replace('.xml', '')
        if not os.path.isdir(file_path):
            os.mkdir(file_path)
        gen_png_from_plist(xml_filename, png_filename, file_path )
    else:
        print "make sure you have boith plist and png files in the same directory"