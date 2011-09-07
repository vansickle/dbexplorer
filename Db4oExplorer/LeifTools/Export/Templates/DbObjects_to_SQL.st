$dbObjects:{ dbObject|
		INSERT INTO() VALUES($dbObject.Fields:{field|$field$};separator=", "$)
}$