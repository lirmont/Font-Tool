<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl" xmlns:target="http://darkabstraction.com/schemas/font.xsd">
  <xsl:output method="xml" encoding="utf-8" omit-xml-declaration="no" indent="yes" />
  <xsl:namespace-alias stylesheet-prefix="target" result-prefix="#default"/>
  <xsl:template match="/target:font">
    <xsl:element name="font" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://darkabstraction.com/schemas/font.xsd">
      <xsl:copy-of select="./@*"/>
      <xsl:element name="name">
        <xsl:copy-of select="./target:name/text()"/>
      </xsl:element>
      <xsl:element name="colors">
        <xsl:copy-of select="./target:colors/node()"/>
      </xsl:element>
      <xsl:element name="sizes">
        <xsl:for-each select="./target:sizes/target:size">
          <xsl:sort select="./@value" data-type="number" order="ascending"/>
          <xsl:element name="size">
            <xsl:copy-of select="./@*"/>
            <xsl:element name="characters">
              <xsl:for-each select="./target:characters/target:character">
                <xsl:sort select="./@code" data-type="number" order="ascending"/>
                <xsl:element name="character">
                  <xsl:for-each select="@*[
                        not(
                            (name()='offset-x' and .='0') or
                            (name()='offset-y' and .='0') or
                            (name()='offset-width' and .='0')
                        )
                     ]">
                    <xsl:copy-of select="."/>
                  </xsl:for-each>
                </xsl:element>
              </xsl:for-each>
            </xsl:element>
            <xsl:element name="variants">
              <xsl:copy-of select="./target:variants/node()"/>
            </xsl:element>
          </xsl:element>
        </xsl:for-each>
      </xsl:element>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>