<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<!-- The exercise name to put at the top of the document -->
	<xsl:template name="LessonNumber">PCE 04</xsl:template>


  <!-- This is for Categories that have a name, but the name doesn't match anything.
			This should never happen 'in production', and will be flagged as an error 
			during the output phase -->
  <xsl:template match="Category[@name!='']" priority="-10">
    <xsl:call-template name="GenerateFailedTest">
      <xsl:with-param name="CategoryName">
        <xsl:value-of select="$Missing_Category"/>
      </xsl:with-param>
      <xsl:with-param name="NodeList" select="." />
      <xsl:with-param name="PointPenalty" select="-1" />
      <xsl:with-param name="Explanation">
        Unable to find a grading category for <xsl:value-of select="@name"/>
      </xsl:with-param>
    </xsl:call-template>
  </xsl:template>

	<xsl:template match="Category[@name='SmartArrayAOD']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-3" />
			<xsl:with-param name="Explanation">
				There is a problem with your "SmartArrayAOD" exercise.
				This tests (generally) work by checking return values from methods, and/or by confirming that values previously placed in the
				SmartArray are still there later, plus some output-checking.
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="Category[@name='LL Add To Front']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-3" />
			<xsl:with-param name="Explanation">There is a problem with your "Add To Front" exercise. 
				This tests (generally) work by checking the actual nodes in the list, after calling AddToFront</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="Category[@name='LL Print All']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-3" />
			<xsl:with-param name="Explanation">
				There is a problem with your "PrintAll" exercise.
				This tests (generally) work by checking the Console output of your method
				If there's a problem with your AddToFront method, that may cause these tests to fail, too
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="Category[@name='LL Remove From Front']">
		<xsl:call-template name="GenerateFailedTest">
			<xsl:with-param name="CategoryName">
				<xsl:value-of select="@name"/>
			</xsl:with-param>
			<xsl:with-param name="NodeList" select="." />
			<xsl:with-param name="PointPenalty" select="-4" />
			<xsl:with-param name="Explanation">
				There is a problem with your "RemoveFromFront" exercise.
				This tests (generally) work by checking the actual nodes in the list, after calling RemoveFromFront.
				If there's a problem with your AddToFront method, that may cause these tests to fail, too
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:stylesheet>

